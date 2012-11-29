package com.github.springaki.codersation;

public enum Money {

	Ten(10), 
	Fifty(50), 
	OneHundred(100), 
	FiveHundred(500), 
	OneThousand(1000);
	
	private int value;
	
	Money(int value) {
		this.value = value;
	}
	
	public int toInt() {
		return value;
	}

}
