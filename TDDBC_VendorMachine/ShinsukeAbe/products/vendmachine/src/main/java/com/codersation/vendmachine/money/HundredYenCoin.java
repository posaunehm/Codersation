package com.codersation.vendmachine.money;

public class HundredYenCoin extends AmountableMoney {
	protected HundredYenCoin() {
	}
	
	@Override
	public Integer getValue() {
		return 100;
	}
}
