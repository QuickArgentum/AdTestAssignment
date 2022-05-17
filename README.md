# Ad Test Assignment
Created using Unity 2020.3.27f1 LTS

To get the project running head to the releases tab and grab the latest release or clone the repository and open it in the Unity editor directly.

# Requirements

<details>
  <summary>Click to view</summary>
  <ol>
  <li>
    Get and present a video ad
    <ul>
      <li>
        Send a GET request to:
        <ul>
        <li>https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast</li>
        </ul>
      </li>
      <li>As a response, you will receive a VAST XML containing a video</li>
      <li>Present the video to the screen</li>
    </ul>
  </li>
  <li>
    Get and present purchase item ad
    <ul>
      <li>
        Send a POST request to:
        <ul>
          <li>https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/ad</li>
          <li>The body should contain any valid JSON</li>
        </ul>
      </li>
      <li>
        As a response, you will receive a JSON containing few details on the purchased item such as:
        <ul>
          <li>Title</li>
          <li>Item_image</li>
          <li>Currency</li>
          <li>Currency_sign</li>
        </ul>
      </li>
      <li>
        Use the response details to present the screen some kind of simple purchase screen containing all the details and ask the user to enter:
        <ul>
          <li>Email</li>
          <li>Credit card number</li>
          <li>Expiration date</li>
        </ul>
      </li>
      <li>
        Send the POST request containing all the information inserted by the user to:
        <ul>
          <li>https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/action</li>
          <li>Body as a JSON</li>
        </ul>
      </li>
      </ul>
    </li>
  </ol>
</details>

# Architecture overview


![diagram](https://user-images.githubusercontent.com/14258721/167814196-e7c6612d-a4c4-4511-9523-b4213d74c59a.png)

The application structure can logically be divided into three main parts: loaders, controller and view.

Loaders handle interaction with external HTTP APIs. For instance, [VideoAdLoader](Assets/Scripts/Loaders/VideoAdLoader.cs) downloads video ad data, parses it and serves it to be displayed in the application.

[App Controller](Assets/Scripts/AppController.cs) handles the interaction between the "moving parts" of the application. It implements the path from view classes to loaders and vice versa.

View classes handle displaying of the relevant data to the user on screen and collecting data from the user. [Start Button Panel](Assets/Scripts/View/StartButtonPanel.cs), for example, controls the two buttons that the user is first presented with and handles their click events as well as controls their behaviour during the application flow.
