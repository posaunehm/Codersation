package com.codersation.abe.vendingmachine.money;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JPY10 implements AmountableMoney {
    
    protected JPY10() {
        
    }

    @Override
    public Integer getValue() {
        return 10;
    }
}
