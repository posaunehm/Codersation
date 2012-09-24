/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine.money;

/**
 *
 * @author mao
 */
class JPY1000 implements AmountableMoney {

    protected JPY1000() {
    }

    @Override
    public Integer getValue() {
        return 1000;
    }
    
}
