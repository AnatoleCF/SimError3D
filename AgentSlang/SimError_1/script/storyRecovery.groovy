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
 * Recover Story From BDD
 *
 * The aim of this script is to recover the story line by line into a file
 * To run it, launch the command in the current folder:
 * groovy storyRecovery.groovy BDD_filename output_filename.txt
 */

@Grab('com.opencsv:opencsv:3.5')
import com.opencsv.CSVReader

if (args.length != 2) {
    println "Usage: storyRecovery.groovy BDD_filename output_filename.txt"
    return
}

def (String bdd, String story) = args

new File(bdd).withReader('UTF-8') {r ->
    new File(story).withWriter('UTF-8') {w ->
        new CSVReader(r, ';' as char).collect {it[2]}.unique().each {
            w.println it
        }
    }
}
