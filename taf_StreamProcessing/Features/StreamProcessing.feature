Feature: StreamProcessing
	Create a Producer to send car messages
	Create a Consumer to receive the messages
	Check that messages produced and consumed are the same

@CarsMessages
Scenario: Produce and Consume Cars Messages 
Given I have cars data messages 
Then I should receive a list of cars messages 
Then The messages received should be same as sent 
