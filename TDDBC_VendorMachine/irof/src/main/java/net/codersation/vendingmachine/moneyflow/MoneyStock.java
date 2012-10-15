package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.Money;

public class MoneyStock {

	private List<Money> stock = new ArrayList<>();

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

	/**
	 * 引数のMoneyStockにこのオブジェクトに全てのStockを移す。移されたほうは空になる。
	 * @param s
	 */
	void moveTo(MoneyStock s) {
		s.stock.addAll(stock);
		stock.clear();
	}

	void remove(Money e) {
		stock.remove(e);
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

	public MoneyStock takeOut(int price) {
		if (price > getAmount())
			throw new IllegalArgumentException("cannot take out. price:"
					+ price + ", amount " + getAmount());
		MoneyStock res = new MoneyStock();
		res.stock = getUseMoneyList(price);
		for (Money money : res.stock) {
			remove(money);
		}
		return res;
	}
}
