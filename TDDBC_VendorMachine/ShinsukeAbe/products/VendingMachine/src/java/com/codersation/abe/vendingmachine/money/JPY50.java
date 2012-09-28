/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine.money;

/**
 *
 * @author mao
 */
public class JPY50 implements AmountableMoney {

    protected JPY50() {
        
    }

    @Override
    public Integer getValue() {
        return 50;
    }
}
