# Rule Pub/Sub

## Objective ##
Indicates how to publish MyGeotab Rule Exception Events to a Google Pub/Sub service instance using a Google function as the intermediary facilitator.

This repo contains an example C# implementation of how to achieve this objective.

## Prerequisites ##
Before proceeding the following prerequisites need to be addressed:
1. A well structured **Notification Template** (**Web Template**) should exist in the relevant MyGeotab database. This template will define the data you expect to receive in the *POST Data* section of the **Web Template**.
2. A MyGeotab database **Rule** should exist with the **Web Request** selected in the **Notifications** tab of the **Rule**.
3. A **Topic** defined in a **Google Pub/Sub** service that matches the **_topicName** path variable defined in this project's code.