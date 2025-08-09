using System;
using System.Text;

public class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country != null && _country.Trim().ToUpper() == "USA" || _country.Trim().ToUpper() == "UNITED STATES" || _country.Trim().ToUpper() == "UNITED STATES OF AMERICA";
    }

    public string GetFullAddress()
    {
        // Return with newline characters where appropriate
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }

    public string GetCountry()
    {
        return _country;
    }
}
