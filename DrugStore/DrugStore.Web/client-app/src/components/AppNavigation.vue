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
          <!-- <img src="" alt="user"> -->
          <v-icon>account_circle</v-icon>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>{{profile.name}}</v-list-item-title>
          <v-list-item-subtitle>{{profile.role}}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </template>
    <v-divider></v-divider>

    <v-list dense>
      <v-list-item :to="{name:'home'}" exact>
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

        <v-list-item :to="{name:'laboratory'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Laboratorios'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'label'"></v-icon>
          </v-list-item-action>  
        </v-list-item>
                  
      </v-list-group>

      <v-list-group
        prepend-icon="credit_card"                     
      >
        <template v-slot:activator>         
          <v-list-item-content>
            <v-list-item-title>Ventas</v-list-item-title>
          </v-list-item-content>                    
        </template>       
     
        <v-list-item :to="{name:'client'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Clientes'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'assignment'"></v-icon>
          </v-list-item-action>  
        </v-list-item>     
                  
      </v-list-group>      

      <v-list-group
        prepend-icon="account_balance_wallet"                     
      >
        <template v-slot:activator>         
          <v-list-item-content>
            <v-list-item-title>Gestion de Pedidos</v-list-item-title>
          </v-list-item-content>                    
        </template>       
     
        <v-list-item :to="{name:'order'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Pedidos'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'assignment'"></v-icon>
          </v-list-item-action>  
        </v-list-item>

        <v-list-item :to="{name:'provider'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Proveedores'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'group_work'"></v-icon>
          </v-list-item-action>  
        </v-list-item>
                  
      </v-list-group>

      <v-list-group
        prepend-icon="account_balance_wallet"                     
       >
        <template v-slot:activator>         
          <v-list-item-content>
            <v-list-item-title>Accesos</v-list-item-title>
          </v-list-item-content>                    
        </template>       
             
        <v-list-item :to="{name:'user'}">
           <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>  
          <v-list-item-content>
            <v-list-item-title v-text="'Usuarios'"></v-list-item-title>                   
          </v-list-item-content>
          <v-list-item-action>
            <v-icon v-text="'group_work'"></v-icon>
          </v-list-item-action>  
        </v-list-item>

         <v-list-item>
          <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>         
          <v-list-item-content>
            <v-list-item-title v-text="'Mi Cuenta'"></v-list-item-title>
          </v-list-item-content>
            <v-list-item-action>
            <v-icon v-text="'account_box'"></v-icon>
          </v-list-item-action>
        </v-list-item>

          <v-list-item :to="{name:'profile'}">
          <v-list-item-action>
            <v-icon></v-icon>
          </v-list-item-action>         
          <v-list-item-content>
            <v-list-item-title v-text="'Seguridad'"></v-list-item-title>
          </v-list-item-content>
            <v-list-item-action>
            <v-icon v-text="'security'"></v-icon>
          </v-list-item-action>
        </v-list-item>
                  
      </v-list-group>
       
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
import axios from 'axios'

export default {
  name: 'AppNavigation',
  data() {
    return {
      drawer: null,
      model:true,
      profile:[]
    }
  },
  created () {
    this.fetchProfile()
  },
  methods: {
    fetchProfile() {
      let me=this;                    
      let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
      let headers = {headers:AuthorizationHeader}
      axios.get('https://localhost:44313/api/user/getProfile/26',headers)
        .then(function (response) {
        // handle success
        me.profile = response.data                             
        })
      .catch(function (error) {
        // handle error          
        console.log(error);         
      })
    }
  },
};
</script>
