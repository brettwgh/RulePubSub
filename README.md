# Rule Pub/Sub

## Project Objective ##
To share an example C# project on how to configure a Google Function to receive Rule Exception events from a MyGeotab database instance and then publish these events to a Google Pub/Sub topic which can then be consumed by an interested party.

### Steps Required ###
This describes the initial steps required (state) to achieve a successful implementation for the above.
1. Create a Notification Template (Web Template) in the relevant MyGeotab database.
  i. Define the necessary data you expect to received in the POST Data area of this Web Template.
2. Create at least one MyGeotab database Rule with the web request set to the template created in the previous step.
3. Create a Google function like this project, deployed to your Google Cloud Project.
4. A topic defined in your active Google Pub/Sub structure that matches the path defined in this project's code.
