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
 * BDD story retranscription empty generator
 *
 * The aim of this script is to generate an empty frame for the the BDD
 * To run it, launch the command in the current folder:
 * groovy emptyGenerator.groovy story_filename.txt BDD_filename.csv
 */

@Grab('com.opencsv:opencsv:3.5')
import com.opencsv.CSVWriter

if (args.length != 2) {
    println "Usage: emptyGenerator.groovy story_filename.txt BDD_filename.csv"
    return
}

def (String story, String fname) = args

def counter = 0

new File(fname).withWriter('UTF-8') {w ->
    def writer = new CSVWriter(w, ';' as char)
    new File(story).eachLine('UTF-8') {line ->
        ++counter
        // counter;preCommands(commandA|commandB|...);story;pattern;answer;postCommands(commandA|commandB|...);nextStep;
        writer.writeNext([counter, '', line.trim(), '', '', '', counter + 1, ''] as String[], false)
    }
}
