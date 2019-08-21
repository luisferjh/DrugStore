<template>
 <span>
    <v-navigation-drawer
      v-model="drawer"
      :clipped="$vuetify.breakpoint.smAndUp"
      app
    > 
    <template v-slot:prepend>
      <v-list-item two-line>
        <v-list-item-avatar>
          <img src="https://randomuser.me/api/portraits/women/81.jpg" alt="user">
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>Jane Smith</v-list-item-title>
          <v-list-item-subtitle>Admin</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </template>
    <v-divider></v-divider>

     <v-list dense>
        <v-list-item :to="{name:'home'}">
          <v-list-item-icon>
            <v-icon>home</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Inicio</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
   
      <v-list-group
        prepend-icon="dashboard"              
       
      >
        <template v-slot:activator>         
            <v-list-item-content>
              <v-list-item-title>Inventario</v-list-item-title>
            </v-list-item-content>                    
        </template>       
     
        <v-list-item :to="{name:'product'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Productos'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'shopping_cart'"></v-icon>
          </v-list-item-action>  
        </v-list-item>
                  
      </v-list-group>

        <v-list-item :to="{name:'about'}">
          <v-list-item-icon>
            <v-icon>shopping_cart</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Ventas</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

         <v-list-item>
          <v-list-item-icon>
            <v-icon>account_box</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Mi Cuenta</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-group
        prepend-icon="dashboard"        
      >
        <template v-slot:activator>
          <v-list-item-content>
            <v-list-item-title v-text="'test'"></v-list-item-title>
          </v-list-item-content>
        </template>

        <v-list-item
          @click=""
        >
          <v-list-item-content>
            <v-list-item-title v-text="'subtest'"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-group>

      </v-list>
    </v-navigation-drawer>
      <v-app-bar 
      :clipped-left="$vuetify.breakpoint.smAndUp"
      app 
      color="teal lighten-1" 
      dark
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Drogueria Colombia's Manager </v-toolbar-title>
    </v-app-bar>
  </span>
</template>

<script>

export default {
  name: 'AppNavigation',
  data() {
    return {
      drawer: null,
      model:true
    }
  },
};
</script>
