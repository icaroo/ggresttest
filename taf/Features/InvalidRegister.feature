Feature: InvalidRegister
	In order to attempt to register
	using only an email
	I want to unsuccessfully register and receive a message error

@mytag
Scenario: InvalidRegister
	Given the endpoint is "register"
	When I register account with "eve.holt@reqres.in"
	Then the response has status code 400
	Then the response should return an error