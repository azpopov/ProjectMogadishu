using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class BuildingCosts
{
    public static ResourceBundle farm = new ResourceBundle(1000, 0, 0);
    public static ResourceBundle embassy = new ResourceBundle(1000, 150, 150);
    public static ResourceBundle shipyard = new ResourceBundle(1500, 0,0);
    public static ResourceBundle huntersLodge = new ResourceBundle(1000, 200, 0);
    public static ResourceBundle goldsmith = new ResourceBundle(1000, 500, 300);
}