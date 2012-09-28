/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine.money;

/**
 *
 * @author mao
 */
class JPY500 implements AmountableMoney {

    protected JPY500() {
        
    }

    @Override
    public Integer getValue() {
        return 500;
    }
    
}
