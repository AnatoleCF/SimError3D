#!/bin/bash

################################################################################
# Copyright (c) Ovidiu Serban, ovidiu@roboslang.org                            #
#               web:http://ovidiu.roboslang.org/                               #
# Authors:      William Boisseleau, william.boisseleau@insa-rouen.fr           #
#               Ovidiu Serban, ovidiu@roboslang.org                            #
# All Rights Reserved. Use is subject to license terms.                        #
#                                                                              #
# This file is part of AgentSlang Project (http://agent.roboslang.org/).       #
#                                                                              #
# AgentSlang is free software: you can redistribute it and/or modify           #
# it under the terms of the GNU Lesser General Public License as published by  #
# the Free Software Foundation, version 3 of the License and CECILL-B.         #
#                                                                              #
# This program is distributed in the hope that it will be useful,              #
# but WITHOUT ANY WARRANTY; without even the implied warranty of               #
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 #
# GNU Lesser General Public License for more details.                          #
#                                                                              #
# You should have received a copy of the GNU Lesser General Public License     #
# along with this program. If not, see <http://www.gnu.org/licenses/>.         #
#                                                                              #
# The CECILL-B license file should be a part of this project. If not,          #
# it could be obtained at  <http://www.cecill.info/>.                          #
#                                                                              #
# The usage of this project makes mandatory the authors citation in            #
# any scientific publication or technical reports. For websites or             #
# research projects the AgentSlang website and logo needs to be linked         #
# in a visible area.                                                           #
################################################################################
#                                                                              #
################################################################################
# Full Data Generator from Story file                                          #
################################################################################
# This is the first script implemented for generating default data depending   #
# on a story.                                                                  #
# To run it, launch the command in the current folder:                         #
# bash storyGenerator.bash story_filename.txt                                  #
# ! commands,story and steps folder will be created in the folder generation.  #
################################################################################

stor=story/Story.xml
com=commands/Commands.xml
ste=

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<story>" > $stor

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<commands>" > $com

counter=0

while read ligne
do	
	counter=$[$counter +1]
	counter2=$[$counter +1]	
	
	# About Story.xml
	echo "<state> <!-- Step n° $counter-->
	<lCommands />
	<story>$ligne</story>
</state>" >> $stor
	
	# About Commands.xml
	echo "<state> <!-- Step n° $counter : $ligne-->
	<command>command $counter</command>
	<lLaunches>
		<launch>tellStory $counter</launch>
		<launch></launch>
		<launch></launch>
	</lLaunches>
</state>" >> $com

	# About Step_i.xml
	echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<!-- Step n° $counter : $ligne -->
<patterns>
    <state>
        <lPatterns>
            <pattern>a$counter</pattern>
            <pattern></pattern>
        </lPatterns>
        <lAnswers>
            <answer></answer>
            <answer></answer>
        </lAnswers>
        <command>command $counter</command>
        <next>$counter2</next>
    </state>
    <state>
        <lPatterns>
            <pattern></pattern>
            <pattern></pattern>
        </lPatterns>
        <lAnswers>
            <answer></answer>
            <answer></answer>
        </lAnswers>
        <command></command>
        <next></next>
    </state>
    <state>
        <lPatterns>
            <pattern>wxcvbn</pattern> <!-- as empty pattern, can be removed -->
        </lPatterns>
        <lAnswers>
            <answer></answer>
            <answer></answer>
        </lAnswers>
        <command></command>
        <next></next>
    </state>
</patterns>
" > steps/Step_$counter.xml
	
	
done < $1


echo "</story>" >> $stor
echo "</commands>" >> $com
echo "Step_i.xml, Commands.xml and Story.xml files successfully generated"
exit 0	
