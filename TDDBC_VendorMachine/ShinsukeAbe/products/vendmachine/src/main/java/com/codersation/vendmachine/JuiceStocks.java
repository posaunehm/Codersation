package com.codersation.vendmachine;

import java.util.ArrayList;
import java.util.List;

import com.codersation.vendmachine.juice.Coke;
import com.codersation.vendmachine.juice.Juice;

public class JuiceStocks {
	private List<JuiceStock> stocks = new ArrayList<JuiceStock>();
	
	protected JuiceStocks() {
		stocks.add(new JuiceStock(new Coke(), 5));
	}
	
	public List<JuiceStock> getAllStocks() {
		return stocks;
	}

	public Boolean isPurchasable(Amount amount, Juice targetJuice) {
		for(JuiceStock stock: stocks) {
			if(stock.getJuice().equals(targetJuice)) {
				if(amount.getTotal() >= stock.getJuice().getPrice()) {
					if(stock.getStockCount() > 0) {
						return true;
					} else {
						return false;
					}
				} else {
					return false;
				}
			}
		}
		// TODO 存在しないジュースが指定された場合
		return false;
	}
}
