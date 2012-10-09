package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.Money;

public class MoneyStock extends ArrayList<Money> {

	public int getAmount() {
		int amount = 0;
		for (Money c : this) {
			amount += c.getValue();
		}
		return amount;
	}

	public List<Money> getUseMoneyList(int i) {

		List<Money> result = new ArrayList<>();

		Collections.sort(this);
		for (Money money : this) {
			if (i <= 0) {
				break;
			}
			if (money.getValue() < i) {
				result.add(money);
				i -= money.getValue();
			} else {
				result.add(money);
				i -= money.getValue();
			}
		}

		for (Money money : result.toArray(new Money[0])) {
			if (i <= money.getValue() * -1) {
				result.remove(money);
				i += money.getValue();
			}
		}
		return result;

	}

	/**
	 * @deprecated 一時的に残す
	 */
	public static List<Money> getUseMoneyList(List<Money> list, int i) {
		MoneyStock stock = new MoneyStock();
		stock.addAll(list);
		return stock.getUseMoneyList(i);
	}
}
