//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
public class ResourceBundle
{
	public int commodity = 0, luxury = 0,  wealth = 0;

	public ResourceBundle (string type, int amount)
	{
		StringAdd (type, amount);
	}

	public ResourceBundle (int _commodities, int _luxuries, int _wealth)
	{
		commodity = _commodities;
		luxury = _luxuries;
		wealth = _wealth;
	}

	public void StringAdd (string type, int amount)
	{
		switch (type) {
		case("Commodity"):
			commodity += amount;
			break;
		case("Luxury"):
			luxury += amount;
			break;
		case("Wealth"):
			wealth += amount;
			break;
		default:
			return;
		}
	}
    //returns true when bundle passed is smaller
    //returns false when bundle passed is larger in ANY resource
    public bool CompareBundle(ResourceBundle _bundle)
    {
        if (_bundle.commodity > commodity || _bundle.luxury > luxury || _bundle.wealth > wealth)
            return false;
        return true;
    }

    public static ResourceBundle operator +(ResourceBundle _bundle1, ResourceBundle _bundle2)
    {
        return new ResourceBundle(_bundle1.commodity + _bundle2.commodity, _bundle1.luxury + _bundle2.luxury, _bundle1.wealth + _bundle2.wealth);
    }

        public static ResourceBundle operator -(ResourceBundle _bundle1, ResourceBundle _bundle2)
    {
        return new ResourceBundle(_bundle1.commodity - _bundle2.commodity, _bundle1.luxury - _bundle2.luxury, _bundle1.wealth - _bundle2.wealth);
    }

        public static ResourceBundle operator *(ResourceBundle _bundle1, float _number)
        {
            return new ResourceBundle((int)(_bundle1.commodity * _number), (int)(_bundle1.luxury * _number), (int)(_bundle1.wealth * _number));
        }

        public int ReturnTypeofMax()
        {

            List<int> intList = new List<int>();
            intList.Add(commodity);
            intList.Add(luxury);
            intList.Add(wealth);
            int maxValue = intList.Max();
            int maxIndex = intList.ToList().IndexOf(maxValue);
            return maxIndex;
        }

        public int ReturnMax()
        {
            List<int> intList = new List<int>();
            intList.Add(commodity);
            intList.Add(luxury);
            intList.Add(wealth);
            return intList.Max();
        }
        public string ReturnStringofMax()
        {
            switch (ReturnTypeofMax())
            {
                case 0:
                    return "Commodity";
                case 1:
                    return "Luxury";
                case 2:
                    return "Wealth";
                default:
                    return "";
            }
        }
}


