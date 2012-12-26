package net.codersation.vendingmachine.money;

public class MoneyPolicy {
    boolean isAllowed(Money money) {
        switch (money) {
            case TenYen:
            case FiftyYen:
            case HundredYen:
            case FiveHundredYen:
            case ThousandYen:
                return true;
            default:
                return false;
        }
    }
}