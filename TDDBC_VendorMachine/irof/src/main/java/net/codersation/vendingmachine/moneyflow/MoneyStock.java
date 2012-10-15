package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.Money;

public class MoneyStock {

	private ArrayList<Money> stock = new ArrayList<>();

	public int getAmount() {
		int amount = 0;
		for (Money c : stock) {
			amount += c.getValue();
		}
		return amount;
	}

	void add(Money e) {
		stock.add(e);
	}

	public void addAll(List<Money> l) {
		stock.addAll(l);
	}

	void addAll(MoneyStock s) {
		stock.addAll(s.stock);
	}

	void remove(Money e) {
		stock.remove(e);
	}

	void clear() {
		stock.clear();
	}

	public List<Money> getUseMoneyList(int i) {

		List<Money> result = new ArrayList<>();

		Collections.sort(stock);
		for (Money money : stock) {
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
}
