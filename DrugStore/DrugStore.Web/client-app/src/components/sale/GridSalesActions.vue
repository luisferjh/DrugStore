<template>
  <v-container>
    <v-row no-gutters>
      <v-card flat color="white"  v-if="!showNewSale">
        <v-card-title>Ventas
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Search"
            v-if="!showNewSale"
            single-line              
          ></v-text-field>
          <v-spacer></v-spacer>          
    
          <template>
            <v-btn color="primary" dark class="mb-2" @click="hideGridSale()">Nueva Venta</v-btn>
          </template>
                      
        </v-card-title> 
        
        <v-data-table        
          :headers="headersDataGrid"
          :items="sales"
          :search="search"
          class="elevation-1"              
        >            
                                                
          <template v-slot:item.state="{item}">                
              <div v-if="item.state === 'Aceptado'">
                <span class="blue--text">Aceptado</span>
              </div>
              <div v-else>
                <span class="red--text">{{item.state}}</span>
              </div>
          </template>

          <template v-slot:item.action="{ item }">         
            <v-icon
              small
              class="mr-2"                  
              @click="showSaleforEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template v-if="item.state === 'Aceptado'">
              <v-icon
                small
                @click=""
              >
                block
              </v-icon>
            </template>
             <template v-else>
              <v-icon
                small
                @click=""
              >
                check
              </v-icon>
            </template>
          </template>  
        </v-data-table>
      </v-card>

      <template>            
        <v-dialog max-width="600px">       
         
                      
        </v-dialog>                              
      </template>

      <template>
        <v-dialog 
          v-model="adModal"
          max-width="290"
          >
            <v-card>
              <v-card-title class="headline" v-if="adAction === 1">¿Activar Producto?</v-card-title>
              <v-card-title class="headline" v-if="adAction === 2">¿Desactivar Producto?</v-card-title>
              <v-card-text>
                Estas a punto de
                <span v-if="adAction === 1">Activar</span>
                <span v-if="adAction === 2">Desactivar</span>
                el producto {{ adName}}
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="green darken-1" flat="flat" @click="adModal=0">Cancelar</v-btn>
                <v-btn v-if="adAction== 1" color="green darken-4" @click="activate()">Aceptar</v-btn>
                <v-btn v-if="adAction== 2" color="green darken-4" @click="deactivate()">Aceptar</v-btn>
              </v-card-actions>
            </v-card>
        </v-dialog>
      </template>
    
      <v-row no-gutters>
        <FormNewSale 
          v-if="showNewSale"
          @cancel="showGridSale" 
        /> 
      </v-row>
    
    </v-row>
  </v-container>
</template>

<script> 
  import axios from 'axios'   
  import FormNewSale from '@/components/sale/FormNewSale'

  export default {
    name:'GridProducts',
    components: {
      FormNewSale
    },
    data() {
      return {
        headersDataGrid:[
          {text:'ID', value:'idSale'},
          {text:'Usuario', value:'userName'},
          {text:'Cliente', value:'clientName'},
          {text:'Tipo de venta', value:'typeSale'},
          {text:'Serie de comprobante', value:'voucherSeries'},
          {text:'numero de comprobante', value:'voucherNumber'},
          {text:'Fecha Venta', value:'saleDate'},
          {text:'Ultima Actualizacion', value:'saleDateUpdate'},
          {text:'Total', value:'totalPrice'},
          {text:'Estado', value:'state'},
          { text: 'Actions', value: 'action', sortable: false }
        ],
        sales:[],
        sale:'',        
        search:'',       
        showNewSale: false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listSales()     
    },   
    methods: {
      listSales() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/sale/list',headers)
        .then(function (response) {
          // handle success
          me.sales = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },              
      hideGridSale(){
        this.showNewSale = true
      },
      showGridSale(){
        this.showNewSale = false
      },
               
    },
  }
</script>
