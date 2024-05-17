module hello.jigsaw {
    requires java.base;
    requires java.sql;
    // requires transitive java.logging; java.sql 依赖 java.logging 
    exports com.loern.jigsaw;
}
