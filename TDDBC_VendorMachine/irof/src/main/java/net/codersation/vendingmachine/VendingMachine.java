package net.codersation.vendingmachine;

import java.util.ArrayList;
import java.util.List;

import net.codersation.vendingmachine.moneyflow.MoneyFlow;
import net.codersation.vendingmachine.moneyflow.MoneyFlowFactory;
import net.codersation.vendingmachine.stockflow.JuiceRack;
import net.codersation.vendingmachine.stockflow.JuiceStock;

public class VendingMachine {

	public MoneyFlow moneyFlow = MoneyFlowFactory.create();
	private JuiceStock juiceStock = new JuiceStock();

	public int getCreditAmount() {
		return moneyFlow.getCreditAmount();
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
		if (!juice.isEnough(getCreditAmount())) {
			return;
		}

		JuiceRack stock = juiceStock.getRack(juice);
		if (stock.isInStock()) {
			stock.remove();
			moneyFlow.purchase(juice.getPrice());
		}
	}

	public int getSaleAmount() {
		return moneyFlow.getSaleAmount();
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (juiceStock.isInStock(juice) && juice.isEnough(getCreditAmount())) {
				list.add(juice);
			}
		}
		return list;
	}

	public List<JuiceRack> getAllJuiceStock() {
		return juiceStock.getRacks();
	}

	public int getStockCount(Juice juice) {
		return juiceStock.getRack(juice).getCount();
	}
}
