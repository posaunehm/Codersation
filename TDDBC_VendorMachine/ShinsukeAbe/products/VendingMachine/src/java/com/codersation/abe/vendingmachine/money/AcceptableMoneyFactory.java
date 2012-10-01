package com.codersation.abe.vendingmachine.money;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class AcceptableMoneyFactory {
    
    public static AcceptableMoney createNewMoney(Integer value) {
        switch(value) {
            case 10: 
            case 50:
            case 100:
            case 500:
            case 1000:
                return new AcceptableMoney(value);
            default: throw new IllegalArgumentException("Unamountable money:" + value.toString());
        }
    }
}
