# Ad Test Assignment
Created using Unity 2020.3.27f1 LTS

To get the project running head to the releases tab and grab the latest release or clone the repository and open it in the Unity editor directly.

# Architecture overview


![diagram](https://user-images.githubusercontent.com/14258721/167814196-e7c6612d-a4c4-4511-9523-b4213d74c59a.png)

The application structure can logically be divided into three main parts: loaders, controller and view.

Loaders handle interaction with external HTTP APIs. For instance, [VideoAdLoader](Assets/Scripts/Loaders/VideoAdLoader.cs) downloads video ad data, parses it and serves it to be displayed in the application.

[App Controller](Assets/Scripts/AppController.cs) handles the interaction between the "moving parts" of the application. It implements the path from view classes to loaders and vice versa.

View classes handle displaying of the relevant data to the user on screen and collecting data from the user. [Start Button Panel](Assets/Scripts/View/StartButtonPanel.cs), for example, controls the two buttons that the user is first presented with and handles their click events as well as controls their behaviour during the application flow.
