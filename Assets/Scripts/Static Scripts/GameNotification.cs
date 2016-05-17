using UnityEngine;
using System.Collections;

public static class GameNotification{
    public const string EmbassyShow = "embassy.show";
    public const string EmbassyFactionChange = "embassy.faction.change";
    public const string ShipyardCreateShipUI = "shipyard.create.ship.ui";
    public const string ResourcePickup = "resource.pickup.mouse";
    public const string ShipTravelEvent = "shipyard.ship.travel.event";
    public const string ShipOnMission = "shipyard.ship.mission.set";
    public const string ResultResourceChange = "resource.change.result.screen";
    public const string AddResources = "manager.model.add.bundle";
    public const string EmbassyFactionInfluence = "embassy.model.faction.influence";
    public const string ErrorShipAvailable = "shipyard.ship.themission.null";
    public const string ErrorBuildingCapacityMax = "building.model.maxresources.true";
    public const string ErrorNoShipAvailable = "sendship.noship.available";
    public const string StoryEventDebt = "event.story.debt.next";
    public const string StoryEventInterest = "event.story.debt.interest.increase";
    public const string ErrorShipEventAvailable = "error.shipyard.ship.event.present";
    public const string GameOver = "game.over";
}
