package com.codersation.abe.vendingmachine.managestock;


import java.util.ArrayList;
import java.util.List;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JuiceStocks {
    
    private List<JuiceStock> stocks = new ArrayList<JuiceStock>();

    public List<JuiceStock> getAll() {
        return stocks;
    }

    public void add(Juice juice, int count) {
        stocks.add(new JuiceStock(juice, count));
    }

    public JuiceStock get(Juice juice) {
        for(JuiceStock stock: stocks) {
            if(stock.getJuice().equals(juice)) {
                return stock;
            }
        }
        return null;
    }
    
}
