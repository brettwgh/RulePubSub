# Rule Pub/Sub

## Objective ##
Indicates how to publish **MyGeotab Rule Exception Events** to a **Google Pub/Sub** service instance using a **Google Function** as the intermediary facilitator.

This repository contains an example C# implementation of the **Google Function** step mentioned above.

## Prerequisites ##
Before proceeding the following prerequisites need to be addressed:
1. A well structured **Notification Template** (**Web Template**) should exist in the relevant MyGeotab database. This template defines the data you expect to receive in the *POST Data* section of the **Web Template**.
2. A MyGeotab database **Rule** should exist with the predefined **Web Request** affiliated to this **Rule**.
3. A **Topic** should exist in a **Google Pub/Sub** service instance that matches the one defined in this project's code (_topicName variable).