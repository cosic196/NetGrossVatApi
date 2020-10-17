Feature: CalculatingNetGrossVatAmounts
	In order to use correctly calculated purchase data
	As an API user
	I want to calculate Net, Gross, VAT amounts for purchases in Austria

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on valid input
	Given one of the net, gross or VAT amounts
	And a valid Austrian VAT rate
	And a valid amount type
	When the API is called
	Then the other two missing amounts are calculated and returned

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on missing amount value
	Given a valid Austrian VAT rate
	And a valid amount type
	When the API is called
	Then the API returns an invalid response with an ArgumentException

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on missing Vat rate
	Given one of the net, gross or VAT amounts
	And a valid amount type
	When the API is called
	Then the API returns an invalid response with an ArgumentException

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on invalid amount type
	Given one of the net, gross or VAT amounts
	And a valid Austrian VAT rate
	When the API is called
	Then the API returns an invalid response with an ArgumentException

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on invalid amount value
	Given an invalid amount value
	And a valid Austrian VAT rate
	And a valid amount type
	When the API is called
	Then the API returns an invalid response with an ArgumentException

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on invalid Vat rate
	Given one of the net, gross or VAT amounts
	And an invalid Austrian VAT rate
	And a valid amount type
	When the API is called
	Then the API returns an invalid response with an ArgumentException