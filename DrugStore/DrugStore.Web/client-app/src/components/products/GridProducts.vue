<template>
  <v-container>
    <v-layout>
      <v-card flat color="white">
        <v-card-title>Productos
         <v-spacer></v-spacer>
         <v-text-field
              v-model="search"
              append-icon="search"
              label="Search"
              single-line              
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-dialog>
              <template v-slot:activator="{ on }">
                <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
              </template>
            </v-dialog>
        </v-card-title> 
        <v-data-table        
          :headers="headersDataGrid"
          :items="products"
          :search="search"
          class="elevation-1"      
        >                    
                                
          <template v-slot:item.stock="{item}">                
            <v-chip :color="getColor(item.stock)" dark>{{item.stock}}</v-chip>
          </template>
          
          <template v-slot:item.condition="{item}">                
              <div v-if="item.condition">
                <span class="blue--text">Activo</span>
              </div>
              <div v-else>
                <span class="red--text">Inactivo</span>
              </div>
          </template>

          <template v-slot:item.action="{ item }"> 
            <v-icon
              small
              class="mr-2"
              @click=""
            >
              edit
            </v-icon>
            <v-icon
              small
              @click=""
            >
              delete
            </v-icon>
          </template>  
        </v-data-table>
      </v-card>
    </v-layout>
  </v-container>
</template>

<script> 
  import axios from 'axios' 
  export default {
    name:'GridProducts',
    data() {
      return {
        headersDataGrid:[
          {text:'ID', value:'idProduct'},
          {text:'Nombre', value:'productName'},
          {text:'Categoria', value:'category'},
          {text:'Laboratorio', value:'laboratory'},
          {text:'Stock', value:'stock'},
          {text:'Precio', value:'price'},
          {text:'Estado', value:'condition'},
          { text: 'Actions', value: 'action', sortable: false }
        ],
        products:[],
        search:''
      }
    },
    created () {
      this.fetchProducts()
    },
    methods: {
      fetchProducts() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('https://localhost:44313/api/product/list',headers)
        .then(function (response) {
          // handle success
          me.products = response.data 
          console.log(me.products)                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);         
        })
      },
      addNewProduct(){

      },
      // colorear el stock deacuerdo a las unidades
      getColor(stock){
        if (stock > 30) return 'green'
        else if (stock > 10) return 'orange'
        else return 'red'
      }
    },
  }
</script>
