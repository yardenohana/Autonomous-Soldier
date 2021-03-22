# Autonomous Soldier
## About The Project

In the future we won't need real-life soldiers to fight, as technology takes over.
This project revolves around simulating a soldier. The agent learns several tasks using the Reinforcement learning method. 
The final Project will include four main simulations, each shows a different skill a soldier is capable of, such as:
* Chase after a target
* Run away from an enemy
* Work in a team to solve a problem
* Jump over obstacles
* Absorb the environment using sensors

### 1. Chase Simulation
This simulation is our basic example, shows a soldier chases another in the arena.
* Chaser gets reward of 1.0 when catching the runner
* Runner gets punishment of -1.0 when being catched

#### Observations - field of view:
* View radius around the agent
* Angle of 180° 
* Unity Raycast for targets inside the angle
* Using Unity layers to determine target is an enemy, or obstacle

### 2. Jumping Simulation
Reveals the ability of the agent to jump over an obstacle, while a different character chases after him.

1) Started with teaching an agent how to jump over an obstacle, in the arenas bounds
2) Added both Chaser & Runner - a duplicate of the original agent brain
3) Trained the Chaser, and Runner, to use the jumping ability while the chase

When the following cases occur the current training episode ends;
* Reward of 1.0 when chaser catches the runner
* Punishment of -1.0 when Runner being catched
* Reward of -1.0 when Runner/Chaser touches the obstacles
* Punishment of -1.0 when Runner/Chaser touches the ceiling
* Punishment of -1.0 when Runner/Chaser move out of the arena's bounds

### 3. Shooting Simulation
Shows an agent who is able to shoot & neutralize a number of enemies approaching him.

1) Trained basic agent to use shooting with raycast, no other abilities for now
2) Added moving ability, trained with static enemy (no movement)
3) Trained the shooter to learn rotation, in order to have proper shooting skills to face with number of enemies
  * Enemy placed randomly every episode, agent only rotates and shoot
4)  Six enemies are added to the scene
5)  Increasing the curiosity of the agent, and using curriculum learning for better results

* Reward of 1.0 divided by the amount of enemies, when shooting an anemy succeeded
* Punishment of -0.02 when missing a shot
* Punishment of -1.0 when being catched
* Punishment of -0.001 each frame, for speeding up the training process

### Reward System
As we are using Reinforcement learning, we train our network using [PPO](https://openai.com/blog/openai-baselines-ppo/) algorithm.
* In order to build the simulations we had to base each one of them of a specific reward/punishment algorithm.
* Look at the Scripts directory in order to understand it better.

### Built With

* [MLAgents](https://github.com/Unity-Technologies/ml-agents)
* [Unity](https://unity3d.com)
* [Tensorflow](https://www.tensorflow.org/)
* C#

### Prerequisites

First make sure to have the next tools.
* Unity 2020 edition
* [Python3](https://www.python.org/downloads/)
* ML-Agents 0.17 library installation:
  ```sh
  pip install mlagents==0.0.17
  ```
* Tensorflow installation:
  ```sh
  pip install tensorflow
  ```
  
### Installation
1. Clone the ml-agents-release-3-branch from [here](https://github.com/Unity-Technologies/ml-agents).
2. Set the branch you desire to use from our project.
3. Clone our branch into the /Project folder in the mlagents local repo.
4. Open one of the examples from:
   - ml-agents-release_3_branch\Project\Assets\ML-Agents\Examples
5. Install mlagents Unity support from the example:
   - Search MLAgents at Window -> Package Manager
6. Use one of our trained brains and run!

<!-- CONTACT -->
## Contact

* Yarden Ohana - yardenohana3@gmail.com
* Liel Friede - lielfriede762003@gmail.com
