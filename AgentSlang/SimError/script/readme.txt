Script functionality and model generation:

1. [SCRIPT]1_BDD_story_retranscriptions_empty_generator -> emptyGenerator.groovy

BDD story retranscription empty generator

The aim of this script is to generate an empty frame for the the BDD
To run it, launch the command in the current folder:
  groovy emptyGenerator.groovy story_filename.txt BDD_filename.csv

2. [SCRIPT]2_Full_Data_generator_from_BDD -> scriptGenerator.groovy

Full Data Generator from BDD

The aim of this script is to generate all data from an filled csv BDD.
To run it, launch the command in the current folder:
  groovy scriptGenerator.groovy BDD_filename.csv
commands,story and steps folder will be created in the folder generation.

3. [SCRIPT]Recover_Story_From_BDD -> storyRecovery.groovy

Recover Story From BDD

The aim of this script is to recover the story line by line into a file
To run it, launch the command in the current folder:
  groovy storyRecovery.groovy BDD_filename.csv output_filename.txt

4. [SCRIPT]storyGeneration -> storyGenerator.bash

Full Data Generator from BDD

This is the first script implemented for generating default data depending on a story.
To run it : 
  bash storyGenerator.bash story_filename.txt
