package com.codersation.vendmachine;

import com.codersation.vendmachine.juice.Juice;

public class JuiceStock {
	private Juice juice;
	
	private Integer stockCount;
	
	public JuiceStock() {
		super();
	}
	
	public JuiceStock(Juice juice, Integer stockCount) {
		this.juice = juice;
		this.stockCount = stockCount;
	}

	public Juice getJuice() {
		return juice;
	}

	public void setJuice(Juice juice) {
		this.juice = juice;
	}

	public Integer getStockCount() {
		return stockCount;
	}

	public void setStockCount(Integer stockCount) {
		this.stockCount = stockCount;
	}
	
	
}
