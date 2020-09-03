#!/usr/bin/env groovy

/*
 * Copyright (c) Ovidiu Serban, ovidiu@roboslang.org
 *               web:http://ovidiu.roboslang.org/
 * Authors:      William Boisseleau, william.boisseleau@insa-rouen.fr
 *               Ovidiu Serban, ovidiu@roboslang.org
 * All Rights Reserved. Use is subject to license terms.
 *
 * This file is part of AgentSlang Project (http://agent.roboslang.org/).
 *
 * AgentSlang is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, version 3 of the License and CECILL-B.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * The CECILL-B license file should be a part of this project. If not,
 * it could be obtained at  <http://www.cecill.info/>.
 *
 * The usage of this project makes mandatory the authors citation in
 * any scientific publication or technical reports. For websites or
 * research projects the AgentSlang website and logo needs to be linked
 * in a visible area.
 */

/*
 * Full Data Generator from BDD
 *
 * The aim of this script is to generate all data from a filled csv BDD.
 * To run it, launch the command in the current folder:
 * groovy scriptGenerator.groovy [-dir generation] BDD_filename.csv
 * ! commands,story and steps folder will be created in the folder generation.
 */

import groovy.xml.MarkupBuilder

@Grab('org.apache.commons:commons-lang3:3.4')
import org.apache.commons.lang3.StringUtils

@Grab('com.opencsv:opencsv:3.5')
import com.opencsv.CSVReader

class Entry {
    int id
    String story
    String pattern
    String answer
    List<String> preCommands
    List<String> postCommands
    String next

    static Entry parse(String[] line) {
        def (id, preCommands, story, pattern, answer, postCommands, next) = line
        new Entry(
            id: id.toInteger(),
            preCommands: StringUtils.split(preCommands, '|'),
            story: story,
            pattern: pattern.empty ? 'wxcvbn' : pattern,
            answer: answer,
            postCommands: StringUtils.split(postCommands, '|'),
            next: next,
        )
    }
}

def cli = new CliBuilder(usage: 'scriptGenerator.groovy [-o outputdir] BDD_filename.csv')
cli.o(args: 1, argName: 'outputdir', 'Define the output directory (default: generation)')
cli.h(longOpt: 'help', 'Show this help message')
def options = cli.parse(args)

if (options.h || options.arguments().size() < 1) {
    cli.usage()
    return
}

def (String bdd) = options.arguments()

File outputDir = new File(options.o ? options.o : 'generation')
for (subdir in ['story', 'steps']) {
    def dir = new File(outputDir, subdir)
    dir.deleteDir()
    dir.mkdirs()
}

List<Entry> entries =
    new File(bdd).withReader('UTF-8') {r ->
        new CSVReader(r, ';' as char).collect {Entry.parse(it)}
    }

new File(outputDir, 'story/Story.xml').withWriter('UTF-8') {w ->
    def xml = new MarkupBuilder(w)
    xml.mkp.xmlDeclaration(version: '1.0', encoding: 'UTF-8')

    xml.story {
        Set<Integer> storyIds = new HashSet()
        for (entry in entries) {
            if (!(storyIds.contains(entry.id))) {
                storyIds << entry.id
                state {
                    mkp.comment "Step #$entry.id"
                    lCommands {
                        entry.preCommands.each {command it}
                    }
                    story entry.story
                }
            }
        }
    }
}

entries.groupBy {it.id}.each {id, entriesForCurrentId ->
    new File(outputDir, "steps/Step_${id}.xml").withWriter('UTF-8') {w ->
        def xml = new MarkupBuilder(w)
        xml.mkp.xmlDeclaration(version: '1.0', encoding: 'UTF-8')

        xml.patterns {
            def states = entriesForCurrentId.groupBy {it.pattern}
            states.each {pattern, entriesWithThatPattern ->
                state {
                    lPatterns {
                        xml.pattern pattern
                    }
                    lAnswers {
                        entriesWithThatPattern*.answer.each {answer it}
                    }
                    lCommands {
                        entriesWithThatPattern[0].postCommands.each {command it}
                    }
                    next entriesWithThatPattern[0].next
                }
            }
        }
    }
}
