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
              <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
            </template>

              <v-card>
                <v-card-title>
                  <span class="headline">Nuevo Producto</span>
                </v-card-title>
                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field label="Nombre del producto*" v-model="nameProduct" required></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="5">
                        <v-select
                          v-model="category"
                          :items="categories"
                          item-text="name"
                          item-value="idCategory"
                          label="Categoria*"
                          required
                        ></v-select>                        
                      </v-col>
                      <v-col cols="12" sm="6" md="5">
                        <v-select
                          v-model="laboratory"
                          :items="laboratories"
                          item-text="laboratoryName"
                          item-value="idLaboratory"
                          label="Laboratorio*"
                          required
                        ></v-select>                         
                      </v-col>
                      <v-col cols="12" md="2">
                        <v-text-field label="Stock*" v-model="stock" required></v-text-field>
                      </v-col>

                      <v-col cols="12" lg="6">
                        <v-menu
                          v-model="menu1"
                          :close-on-content-click="false"
                          full-width
                          max-width="290"
                        >
                          <template v-slot:activator="{ on }">
                            <v-text-field
                              v-model="dueDate"
                              clearable
                              prepend-icon="event"
                              label="Fecha de vencimiento"                              
                              v-on="on"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            v-model="dueDate"
                            no-title
                            @change="menu1 = false"
                          ></v-date-picker>
                        </v-menu>
                      </v-col>

                      <v-col cols="12">
                        <v-text-field label="Codigo de barra*" v-model="barCode"></v-text-field>
                      </v-col>                     

                       <v-col cols="12">
                        <v-text-field label="Indicativo*" v-model="indicative" ></v-text-field>
                      </v-col>

                      <v-col cols="12">
                        <v-text-field label="Precio*" v-model="price" required></v-text-field>
                      </v-col>
                      <v-col cols="12">
                        
                        <v-select
                          v-model="estado"
                          :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
                          item-text="estado"
                          item-value="value"                       
                          label="Estado*"
                          required
                        ></v-select>  
                      </v-col>
                    </v-row>
                  </v-container>
                  <small>*indica campo obligatorio</small>
                  
                </v-card-text>
                <v-card-actions>
                  <div class="flex-grow-1"></div>
                  <v-btn color="blue darken-1" text @click="dialogNewProduct = false">Cerrar</v-btn>
                  <v-btn color="blue darken-1" text @click="addNewProduct()">Guardar</v-btn>
                </v-card-actions>
              </v-card>
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
              @click.stop="showProductforEdit(item)"
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

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <v-card>
            <v-card-title>
              <span class="headline">Editar Producto</span>
            </v-card-title>
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12" sm="6" md="12">
                      <v-text-field label="Nombre del producto*" v-model="product.productName" required></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="5">
                      <v-select
                        v-model="product"
                        :items="categories"
                        item-text="name"
                        item-value="idCategory"
                        label="Categoria*"
                        required
                      ></v-select>                        
                    </v-col>
                    <v-col cols="12" sm="6" md="5">
                      <v-select
                        v-model="product"
                        :items="laboratories"
                        item-text="laboratoryName"
                        item-value="idLaboratory"
                        label="Laboratorio*"
                        required
                      ></v-select>                         
                    </v-col>
                    <v-col cols="12" md="2">
                      <v-text-field label="Stock*" v-model="product.stock" required></v-text-field>
                    </v-col>                   

                    <v-col cols="12">
                      <v-text-field label="Codigo de barra*" v-model="product.barCode"></v-text-field>
                    </v-col>                     

                      <v-col cols="12">
                      <v-text-field label="Indicativo*" v-model="product.indicative" ></v-text-field>
                    </v-col>

                    <v-col cols="12">
                      <v-text-field label="Precio*" v-model="product.price" required></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      
                      <v-select
                        v-model="product.condition"
                        :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
                        item-text="estado"
                        item-value="value"                       
                        label="Estado*"
                        required
                      ></v-select>  
                    </v-col>
                  </v-row>
                </v-container>
                <small>*indica campo obligatorio</small>
                
              </v-card-text>
              <v-card-actions>
                <div class="flex-grow-1"></div>
                <v-btn color="blue darken-1" text @click="dialogEdit = false">Cerrar</v-btn>
                <v-btn color="blue darken-1" text @click="updateProduct()">Guardar</v-btn>
              </v-card-actions>
          </v-card>                               
        </v-dialog>                              
      </template>

    </v-layout>
  </v-container>
</template>

<script> 
  import axios from 'axios' 
  import { type } from 'os';
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
        product:'',
        nameProduct:'',
        category:'',
        laboratory:'',
        stock:0,
        barCode:'',
        indicative:'',
        price:'',
        estado:'',
        search:'',
        categories:[],
        laboratories:[],
        dialogNewProduct:false,
        dialogEdit:false,
        dueDate: new Date().toISOString().substr(0, 10),
        menu1: false
      }
    },
    created () {
      this.fetchProducts()
      this.fetchCategories()
      this.fetchLaboratories()
    },   
    methods: {
      fetchProducts() {
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
      addNewProduct(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/product/Create/',
          {
            'IdCategory':me.category,
            'IdLaboratory':me.laboratory,
            'ProductName':me.nameProduct,
            'Stock':me.stock,
            'Indicative':me.indicative,
            'DueDate':me.dueDate,
            'Barcode':me.barCode,
            'Price':parseFloat(me.price),
            'Condition':me.estado
          },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          this.dialogNewProduct = false                                          
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
        this.product =    
        {
          'idProduct':item.idProduct,
          'idCategory':item.idCategory,
          'category':item.category,
          'idLaboratory':item.idLaboratory,
          'laboratory':item.laboratory,
          'productName':item.productName,
          'stock':item.stock,
          'indicative':item.indicative,      
          'barCode':item.barCode,
          'price':item.price,
          'condition':item.condition
        }       
        this.dialogEdit = true      
      },
      updateProduct(){
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
            'Price':parseFloat(me.product.price),
            'Condition':me.product.condition
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')                    
          this.dialogEdit = false                                                    
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
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
