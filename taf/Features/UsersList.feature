Feature: RequestUsersList
	Given reqres api endpoint users
	When I retrieve a list of users
	Then I should receive the users list

@GetUsersList
Scenario: Request Users List
	Given the endpoint name is "users"
	When I retrieve the Users List
	Then the response has status code 200
	Then all users are listed in the response

	