/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package megascus.util;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 *
 * @author megascus
 */
public final class ListUtils {
    
    private ListUtils() {}
    
    /**
     * 引数のListを作成します。
     * 
     * @param <T>
     * @param arg
     * @return 
     */
    public static <T> List<T> of(T arg) {
        List<T> list = new ArrayList<>();
        list.add(arg);
        return list;
    }
    
    /**
     * 引数のListを作成します。
     * 
     * @param <T>
     * @param first
     * @param rest
     * @return 
     */
    public static <T> List<T> of(T first, T... rest) {
        List<T> list = new ArrayList<>();
        list.add(first);
        list.addAll(Arrays.asList(rest));
        return list;
    }
    
}
