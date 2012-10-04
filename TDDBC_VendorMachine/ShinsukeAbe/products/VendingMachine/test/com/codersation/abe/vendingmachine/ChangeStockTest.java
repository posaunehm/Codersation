/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine;

import com.codersation.abe.vendingmachine.money.AcceptableMoneyFactory;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.hamcrest.core.Is.*;

/**
 *
 * @author mao
 */
public class ChangeStockTest {
    
    public ChangeStockTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 釣り銭ストックが初期状態で釣り銭に80円を要求すると50円１枚と10円が3枚で計算される
    @Test
    public void 釣り銭ストックの初期状態では各金種が10枚ずつ存在する() {
        ChangeStock changeStock = ChangeStockFactory.createNewChangeStock();
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(10)), is(10));
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(50)), is(10));
    }
}
