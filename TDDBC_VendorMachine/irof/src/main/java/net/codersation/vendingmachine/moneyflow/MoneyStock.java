package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;

import net.codersation.vendingmachine.Money;

public class MoneyStock extends ArrayList<Money> {

	public int getAmount() {
		int amount = 0;
		for (Money c : this) {
			amount += c.getValue();
		}
		return amount;
	}
}
