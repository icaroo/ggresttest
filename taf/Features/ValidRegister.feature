Feature: ValidRegister
	In order to register
	using a valid email and password
	I want to sucessfully register 

@mytag
Scenario: ValidRegister
	Given the endpoint is "register"
	When I register account with "eve.holt@reqres.in" and "pistol"
	Then the response has status code 200
	Then the response should return a token