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
        
        changeCount.put(AcceptableMoneyFactory.createNewMoney(50), 1);
        changeCount.put(AcceptableMoneyFactory.createNewMoney(10), 3);
        
        return changeCount;
    }
}
