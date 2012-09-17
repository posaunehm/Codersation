package com.codersation.vendmachine.juice;

public class Coke extends Juice {
	@Override
	public String getName() {
		return "コーラ";
	}
	
	@Override
	public Integer getPrice() {
		return 120;
	}
	
	@Override
	public boolean equals(Object that) {
		if(that instanceof Coke) {
			Coke thatCoke = (Coke)that;
			if(getName().equals(thatCoke.getName()) &&
					getPrice().equals(thatCoke.getPrice())) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}
