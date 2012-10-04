/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine;

import com.codersation.abe.vendingmachine.money.AcceptableMoney;
import com.codersation.abe.vendingmachine.money.AcceptableMoneyFactory;
import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author mao
 */
class ChangeStock {
    public Integer get(AcceptableMoney money) {
        return 10;
    }

    Map<AcceptableMoney, Integer> calculateCount(Integer change) {
        Map<AcceptableMoney, Integer> changeCount = new HashMap<AcceptableMoney, Integer>();
        
        Integer balance = change;
        
        if(balance / 500 > 0) {
            changeCount.put(AcceptableMoneyFactory.createNewMoney(500), balance / 500);
            balance = balance % 500;
        }
        
        if(balance / 100 > 0) {
            changeCount.put(AcceptableMoneyFactory.createNewMoney(100), balance / 100);
            balance = balance % 100;
        }
        
        if(balance / 50 > 0) {
            changeCount.put(AcceptableMoneyFactory.createNewMoney(50), balance / 50);
            balance = balance % 50;
        }
        
        if(balance / 10 > 0) {
            changeCount.put(AcceptableMoneyFactory.createNewMoney(10), balance / 10);
            balance = balance % 10;
        }
        
        return changeCount;
    }
}
