# Rule Pub/Sub

## Project Objective ##
To indicate how to configure a Google Function to receive Rule Exception events from a MyGeotab database instance and then publish them to a Google Pub/Sub topic.

### Steps Required ###
1. Notification Template (Web Template) in the MyGeotab database.
2. At least one MyGeotab Rule with the web request set to the template created in the previous step.
3. A Google function like this project deployed to your Google Cloud Project.
4. A topic defined in your active Google Pub/Sub structure that matches the path defined in this project's code.
