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

	/**
	 * 引数のMoneyStockにこのオブジェクトに全てのStockを移す。移されたほうは空になる。
	 * @param s
	 */
	void moveAllMoneyTo(MoneyStock s) {
		s.stock.addAll(stock);
		stock.clear();
	}

	private List<Money> getUseMoneyList(int i) {
		// TODO 指定された金額のお金を出すための見苦しいコード

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

	/**
	 * 指定された金額のMoneStockを取り出す。
	 * 取り出された分の金額は減る。
	 * TODO 丁度、多め、などは TakeOutRule とかにしたい
	 * @param price 取り出したい金額
	 * @return 取り出されたMoneyStock
	 */
	public MoneyStock takeOut(int price) {
		if (price > getAmount())
			throw new IllegalArgumentException("cannot take out. price:" + price + ", amount " + getAmount());
		MoneyStock res = new MoneyStock();
		res.stock = getUseMoneyList(price);
		for (Money money : res.stock) {
			remove(money);
		}
		return res;
	}

	private void remove(Money e) {
		stock.remove(e);
	}

	public boolean canTakeOut(int amount) {
		return true;
	}
}
