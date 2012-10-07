package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import net.codersation.vendingmachine.moneyflow.MoneyFlow;
import net.codersation.vendingmachine.stockflow.JuiceRack;
import net.codersation.vendingmachine.stockflow.JuiceStock;

public class VendingMachine {

	public MoneyFlow moneyFlow = new MoneyFlow();
	private JuiceStock juiceStock = new JuiceStock();

	public int getTotalAmount() {
		return moneyFlow.getTotalAmount();
	}

	public void insert(Money money) {
		moneyFlow.insert(money);
	}

	public void payBack() {
		moneyFlow.payBack();
	}

	public int getChangeAmount() {
		return moneyFlow.getChangeAmount();
	}

	public void purchase(Juice juice) {
		JuiceRack stock = juiceStock.getRack(juice);
		if (stock.canPurchase(getTotalAmount())) {
			stock.remove();

			moneyFlow.addSale(juice.getPrice());
			
			List<Money> useMoneyList = CreditService.getUseMoneyList(moneyFlow.credit, juice.getPrice());
			int tempAmount = 0;
			for (Money money : useMoneyList) {
				moneyFlow.credit.remove(money);
				tempAmount += money.getValue();
			}
			if (tempAmount != juice.getPrice()) {
				List<Money> list = new ArrayList<>(Collections.nCopies(100, Money.TenYen));
				List<Money> l = CreditService.getUseMoneyList(list, tempAmount - juice.getPrice());
				moneyFlow.credit.addAll(l);
			}
		}
	}

	public int getSaleAmount() {
		return moneyFlow.getSaleAmount();
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (juiceStock.getRack(juice).canPurchase(getTotalAmount())) {
				list.add(juice);
			}
		}
		return list;
	}

	public List<JuiceRack> getAllJuiceStock() {
		return juiceStock.getRacks();
	}
}
