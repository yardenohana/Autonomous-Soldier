# Autonomous Soldier

## About The Project

This project revolves around simulating a soldier. The agent learns several tasks using the Reinforcement learning method. 
The final Project will include main three simulations which shows different skills a soldier is capable of, such as:
* Chase after a target
* Run away from an enemy
* Work in a team to solve a problem
* Jump over obstacles

### Chase Simulation
This simulation is our basic example, shows a soldier chases another in the arena.

### Jumping Simulation
This simulation reveals the ability of the agent to jump over an obstacle, while a different character chases after him.

### Team Simulation
Here, we have two teams - chasers and runners, who will have to use their environment to win. 
* Each team is connected to one "brain"
* The Runners have the ability to grab obstacles in the arena
* The Runners have the ability to place and lock an obstacle in place.
* The Chasers goal is to touch the Runners.

### Reward System
As we are using Reinforcement learning, we train our network using [PPO](https://openai.com/blog/openai-baselines-ppo/) algorithm.

### Built With

* [MLAgents](https://github.com/Unity-Technologies/ml-agents)
* [Unity](https://unity3d.com)
* [Tensorflow](https://www.tensorflow.org/)
* C#

### Prerequisites

First make sure to have the next tools.
* Unity 2020 edition
* [Python3](https://www.python.org/downloads/)
* ML-Agents 0.17 library
  ```sh
  pip install mlagents==0.0.17
  ```
* Tensorflow
```sh
  pip install tensorflow
  ```
  
### Installation
1) Clone the ml-agents-release-3-branch from [here](https://github.com/Unity-Technologies/ml-agents).
2) Set the branch you desire to use from our project.
3) Clone our branch into the /Project folder in the mlagents local repo.
4) Open one of the examples from:
  * ml-agents-release_3_branch\Project\Assets\ML-Agents\Examples
5) Install mlagents Unity support from the example:
  * Search MLAgents at Window -> Package Manager
6) Use one of our trained brains and run!

<!-- CONTACT -->
## Contact

Yarden Ohana - yardenohana3@gmail.com
Liel Friede - lielfriede762003@gmail.com
