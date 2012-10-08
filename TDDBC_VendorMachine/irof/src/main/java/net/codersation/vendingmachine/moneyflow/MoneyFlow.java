package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.CreditService;
import net.codersation.vendingmachine.Money;

public class MoneyFlow {

	public MoneyPolicy moneyPoricy = new MoneyPolicy();

	private int saleAmount = 0;
	private MoneyStock credit = new MoneyStock();
	private MoneyStock change = new MoneyStock();

	public int getSaleAmount() {
		return saleAmount;
	}

	public void addSale(int price) {
		saleAmount += price;
	}

	public int getTotalAmount() {
		int totalAmount = 0;
		for (Money c : credit) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

	public void insert(Money money) {
		if (moneyPoricy.isAllowed(money)) {
			credit.add(money);
		} else {
			change.add(money);
		}
	}

	public void payBack() {
		change.addAll(credit);
		credit.clear();
	}

	public int getChangeAmount() {
		int totalAmount = 0 ;
		for (Money c : change) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

	public void purchase(int price) {
		addSale(price);
		
		List<Money> useMoneyList = CreditService.getUseMoneyList(credit, price);
		int tempAmount = 0;
		for (Money money : useMoneyList) {
			credit.remove(money);
			tempAmount += money.getValue();
		}
		if (tempAmount != price) {
			List<Money> list = new ArrayList<>(Collections.nCopies(100, Money.TenYen));
			List<Money> l = CreditService.getUseMoneyList(list, tempAmount - price);
			credit.addAll(l);
		}
	}
}
