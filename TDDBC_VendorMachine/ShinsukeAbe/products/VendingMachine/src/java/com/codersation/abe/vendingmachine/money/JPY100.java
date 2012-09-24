package com.codersation.abe.vendingmachine.money;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JPY100 implements AmountableMoney {

    protected JPY100() {
        
    }

    @Override
    public Integer getValue() {
        return 100;
    }
    
}
