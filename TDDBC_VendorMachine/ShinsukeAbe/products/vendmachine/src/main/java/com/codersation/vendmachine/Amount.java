package com.codersation.vendmachine;

import java.util.ArrayList;
import java.util.List;

import com.codersation.vendmachine.money.AmountableMoney;

public class Amount {
	
	private List<AmountableMoney> amountList = new ArrayList<AmountableMoney>();

	public Integer getTotal() {
		Integer totalAmount = 0;
		
		for(AmountableMoney amount: amountList) {
			totalAmount += amount.getValue();
		}
		return totalAmount;
	}

	public void insert(AmountableMoney newAmount) {
		amountList.add(newAmount);
	}

	public Integer payBack() {
		Integer payBackAmount = getTotal();
		
		amountList = new ArrayList<AmountableMoney>();
		
		return payBackAmount;
	}

}
