Feature: CalculatingNetGrossVatAmounts
	In order to use correctly calculated purchase data
	As an API user
	I want to calculate Net, Gross, VAT amounts for purchases

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on valid input
	Given a valid net amount
	And a valid VAT rate
	When the API is called
	Then the other two missing amounts are calculated and returned

@net_gross_vat_calculation
Scenario: Generating an error message when missing vat rate
	Given a valid net amount
	When the API is called
	Then a validation error response is returned
	And the response contains error message: "The vatRate field is required."

@net_gross_vat_calculation
Scenario: Generating an error message when missing an amount value
	Given a valid VAT rate
	When the API is called
	Then a validation error response is returned
	And the response contains error message: "Only one of 'GrossAmount,NetAmount,VatAmount' required."

@net_gross_vat_calculation
Scenario: Generating an error message when using multiple amount values
	Given a valid net amount
	And a valid gross amount
	And a valid VAT rate
	When the API is called
	Then a validation error response is returned
	And the response contains error message: "Only one of 'GrossAmount,NetAmount,VatAmount' required."

@net_gross_vat_calculation
Scenario: Generating multiple input validation error messages
	Given a valid net amount
	And a valid gross amount
	When the API is called
	Then a validation error response is returned
	And the response contains error message: "The vatRate field is required."
	And the response contains error message: "Only one of 'GrossAmount,NetAmount,VatAmount' required."
	
@net_gross_vat_calculation
Scenario: Generating multiple business logic error messages
	Given an invalid net amount
	And an invalid VAT rate
	When the API is called
	Then an unsucessful response is returned
	And the response contains error message: "Can't use a negative or zero amount for money within purchases."
	And the response contains error message: "Invalid VAT rate. VAT rate must be higher than 0 and lower than 1."