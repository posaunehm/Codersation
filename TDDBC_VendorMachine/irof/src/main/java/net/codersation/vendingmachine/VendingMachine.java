package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.stockflow.JuiceRack;
import net.codersation.vendingmachine.stockflow.JuiceStock;

public class VendingMachine {

	private List<Money> credit = new ArrayList<>();
	
	private List<Money> change = new ArrayList<>();
	private MoneyPolicy moneyPoricy = new MoneyPolicy();
	private JuiceStock juiceStock = new JuiceStock();
	private int saleAmount = 0;

	public VendingMachine() {
		for (Juice juice : Juice.values()) {
			juiceStock.add(new JuiceRack(juice, 5));
		}
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

	public void purchase(Juice juice) {
		JuiceRack stock = getJuiceStock(juice);
		if (stock.canPurchase(getTotalAmount())) {
			stock.remove();

			saleAmount += juice.getPrice();
			
			List<Money> useMoneyList = CreditService.getUseMoneyList(credit, juice.getPrice());
			int tempAmount = 0;
			for (Money money : useMoneyList) {
				credit.remove(money);
				tempAmount += money.getValue();
			}
			if (tempAmount != juice.getPrice()) {
				List<Money> list = new ArrayList<>(Collections.nCopies(100, Money.TenYen));
				List<Money> l = CreditService.getUseMoneyList(list, tempAmount - juice.getPrice());
				credit.addAll(l);
			}
		}
	}

	private JuiceRack getJuiceStock(Juice juice) {
		for (JuiceRack stock : juiceStock) {
			if (stock.getJuice().equals(juice)) {
				return stock;
			}
		}
		throw new IllegalStateException("そんなStockはない");
	}

	public int getSaleAmount() {
		return saleAmount;
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (getJuiceStock(juice).canPurchase(getTotalAmount())) {
				list.add(juice);
			}
		}
		return list;
	}

	public List<JuiceRack> getAllJuiceStock() {
		return juiceStock;
	}
}
