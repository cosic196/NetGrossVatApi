Feature: CalculatingNetGrossVatAmounts
	In order to use correctly calculated purchase data
	As an API user
	I want to calculate Net, Gross, VAT amounts for purchases in Austria

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on valid input
	Given one of the net, gross or VAT amounts
	And a valid Austrian VAT rate
	When the API is called
	Then the other two missing amounts are calculated and returned

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on missing amount value
	Given a valid Austrian VAT rate
	When the API is called
	Then the API returns a response with 'Please provide only vatRate and one of the following: vatAmount netAmount grossAmount ' as the error message

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on missing Vat rate
	Given one of the net, gross or VAT amounts
	When the API is called
	Then the API returns a response with 'Please provide vatRate.' as the error message

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on more than one amount type
	Given one of the net, gross or VAT amounts
	And a valid Austrian VAT rate
	When the API is called with more than one amount type
	Then the API returns a response with 'Please provide only vatRate and one of the following: vatAmount netAmount grossAmount ' as the error message

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on negative amount values
	Given an invalid amount value
	And a valid Austrian VAT rate
	When the API is called
	Then the API returns a response with 'Amount needs to be higher than 0.' as the error message

@net_gross_vat_calculation
Scenario: Calculating Net, Gross and Vat amounts based on negative Vat rate
	Given one of the net, gross or VAT amounts
	And an invalid Austrian VAT rate
	When the API is called
	Then the API returns a response with 'vatRate needs to be higher than 0.' as the error message