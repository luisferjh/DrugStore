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

          <!-- Dialog for adding new product -->
          <v-dialog v-model="dialogNewProduct" persistent max-width="600px">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Producto</v-btn>
            </template>
              <FormNewProduct                 
                :categories="categories"
                :laboratories="laboratories"
                @save="addNewProduct"
                @cancel="closeDialog"
              />       
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
              @click="showProductforEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template v-if="item.condition">
              <v-icon
                small
                @click="activateDeactivateShowProduct(2,item)"
              >
                block
              </v-icon>
            </template>
             <template v-else>
              <v-icon
                small
                @click="activateDeactivateShowProduct(1,item)"
              >
                check
              </v-icon>
            </template>
          </template>  
        </v-data-table>
      </v-card>

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <FormEditProduct
            :product="product" 
            :categories="categories"
            :laboratories="laboratories"             
            @save="updateProduct"
            @cancel="closeDialog"
          />  
                      
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

    </v-layout>
  </v-container>
</template>

<script> 
  import axios from 'axios'   
  import FormNewProduct from '@/components/products/FormNewProduct'
  import FormEditProduct from '@/components/products/FormEditProduct'

  export default {
    name:'GridProducts',
    components: {
      FormNewProduct,
      FormEditProduct
    },
    data() {
      return {
        headersDataGrid:[
          {text:'ID', value:'idProduct'},
          {text:'Nombre', value:'productName'},
          {text:'Categoria', value:'category'},
          {text:'Laboratorio', value:'laboratory'},
          {text:'Stock', value:'stock'},
          {text:'Precio Unit', value:'unitPrice'},
          {text:'Precio', value:'salePrice'},
          {text:'Estado', value:'condition'},
          { text: 'Actions', value: 'action', sortable: false }
        ],
        products:[],
        product:'',        
        search:'',
        categories:[],
        laboratories:[],
        dialogNewProduct:false,
        dialogEdit:false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listProducts()
      this.fetchCategories()
      this.fetchLaboratories()
    },   
    methods: {
      listProducts() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/product/list',headers)
        .then(function (response) {
          // handle success
          me.products = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },
      fetchCategories(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/category/list',headers)
        .then(function (response) {
          // handle success
          me.categories = response.data   
          // console.log(response.data )                                               
          })
        .catch(function (error) {
          // handle error          
          console.log(error); 
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }        
        })
      },    
      fetchLaboratories(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/laboratory/list',headers)
        .then(function (response) {
          // handle success
          me.laboratories = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      addNewProduct(product){   
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/product/Create/',
          {
            'IdCategory':product.idCategory,
            'IdLaboratory':product.idLaboratory,
            'ProductName':product.productName,
            'Stock':product.stock,
            'Indicative':product.indicative,            
            'BarCode':product.barCode,
            'UnitPrice':product.unitPrice,
            'SalePrice':product.salePrice,
            'Condition':product.condition
          },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listProducts()
          me.closeDialog()                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      //get by id
      showProductforEdit(item){                    
        this.product = item       
        this.dialogEdit = true      
      },
      updateProduct(item){
        this.product = item.product
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}              
        axios.put('api/product/update',
        {
          'IdProduct':me.product.idProduct,
          'IdCategory':me.product.idCategory,
          'IdLaboratory':me.product.idLaboratory,
          'ProductName':me.product.productName,
          'Stock':me.product.stock,
          'Indicative':me.product.indicative,      
          'Barcode':me.product.barCode,
          'UnitPrice':parseFloat(me.prodcut.unitPrice),
          'SalePrice':parseFloat(me.product.salePrice)       
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')       
          me.closeDialog()     
          me.listProducts()                                                                      
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      activateDeactivateShowProduct(action, item){
        this.adModal = 1 // se muestra el modal de confirmacion
        this.adName = item.productName
        this.adId =  item.idProduct      

        if(action === 1)
        {
          this.adAction = 1
        }
        else if(action === 2)
        {
           this.adAction = 2
        }
        else{
          this.adModal = 0
        }
      },
      activate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/product/Activate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listProducts()                                                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      deactivate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/product/Deactivate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listProducts()                                                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      clearForm(){

      },
      closeDialog(switchBool){
        this.dialogEdit = switchBool
        this.dialogNewProduct = switchBool
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
