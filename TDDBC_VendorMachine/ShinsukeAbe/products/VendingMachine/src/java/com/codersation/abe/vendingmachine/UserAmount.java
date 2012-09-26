package com.codersation.abe.vendingmachine;


import com.codersation.abe.vendingmachine.money.AmountableMoney;
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
public class UserAmount {
    private List<AmountableMoney> amountList = new ArrayList<AmountableMoney>();
    
    public Integer getTotal() {
        Integer totalAmount = 0;
        
        for(AmountableMoney amount: amountList) {
            totalAmount += amount.getValue();
        }
        
        return totalAmount;
    }
    
    public void insert(AmountableMoney money) {
        amountList.add(money);
    }

    public Integer payBack() {
        Integer totalAmount = getTotal();
        amountList.clear();
        
        return totalAmount;
    }
}
